import asyncio
from threading import Thread

import serial
import websockets

SERIAL_PORT = 'COM8'
BAUD_RATE = 9600

clients = []

ser = serial.Serial(SERIAL_PORT, BAUD_RATE)

loop = asyncio.get_event_loop()


async def serial_server(websocket, path):
    clients.append(websocket)
    print("{} connected".format(websocket))
    try:
        while True:
            message = await websocket.recv()
            print("Received {}".format(message))
    except:
        pass
    finally:
        print("{} disconnected".format(websocket))
        clients.remove(websocket)

async def send_message(message):
    for client in clients:
        await client.send(message)

async def read_serial():
    while ser.isOpen():
        message = await loop.run_in_executor(None, ser.readline)
        await send_message(message)

start_server = websockets.serve(serial_server, port=8765)
loop.run_until_complete(start_server)
loop.run_until_complete(read_serial())

ser.close()
