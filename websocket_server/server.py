import asyncio
from threading import Thread

import serial
import websockets

SERIAL_PORT = 'COM8'
BAUD_RATE = 9600

clients = []

ser = serial.Serial(SERIAL_PORT, BAUD_RATE)


@asyncio.coroutine
async def serial_server(websocket, path):
    clients.append(websocket)
    try:
        while True:
            message = await websocket.recv()
            print("Received {}".format(message))
    finally:
        clients.remove(websocket)


def read_serial():
    while ser.isOpen():
        print("Received: {}".format(ser.readline()))


thread = Thread(target=read_serial)
thread.start()

loop = asyncio.get_event_loop()
start_server = websockets.serve(serial_server, port=8765)
loop.run_until_complete(start_server)
loop.run_forever()

ser.close()
thread.join()
