import asyncio
import serial
import websockets

SERIAL_PORT = 'COM8'
BAUD_RATE = 9600

clients = []

ser = serial.Serial(SERIAL_PORT, BAUD_RATE)

# code here

loop = asyncio.get_event_loop()

ser.close()
