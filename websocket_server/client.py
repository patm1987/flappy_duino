import asyncio
import websockets


@asyncio.coroutine
def hello():
    websocket = yield from websockets.connect('ws://192.168.1.192:8765/')

    try:
        name = input("What's your name? ")

        yield from websocket.send(name)
        print("> {}".format(name))

        greeting = yield from websocket.recv()
        print("< {}".format(greeting))

    finally:
        yield from websocket.close()


asyncio.get_event_loop().run_until_complete(hello())
