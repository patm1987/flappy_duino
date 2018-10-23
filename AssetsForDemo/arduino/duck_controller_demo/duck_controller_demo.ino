#include <Adafruit_NeoPixel.h>

bool isDigit(char c);
int asciiToIntValue(char c);
void parseCharacter(char character);

const int analogReadPin = 0;

const int pixelPin = 6;
const int ledPin = 13;

Adafruit_NeoPixel pixel = Adafruit_NeoPixel(1, pixelPin, NEO_GRB + NEO_KHZ800);

enum SerialState {
    WaitingStart,
    GotC,
    ParsingRed,
    ParsingGreen,
    ParsingBlue,
};

SerialState state = SerialState::WaitingStart;

int red = 0;
int green = 0;
int blue = 0;

void setup()
{
    Serial.begin(9600);
    pinMode(13, OUTPUT);
    pixel.begin();
    pixel.show();
}

void loop()
{
    int reading = analogRead(analogReadPin);
    Serial.print("Reading = ")
    Serial.println(reading);
    
    pixel.setPixelColor(0, pixel.Color(reading/4, 255-reading/4, 0));
    pixel.show();
}


bool isDigit(char c)
{
    return c >= '0' && c <= '9';
}

int asciiToIntValue(char c)
{
    return c - '0';
}

/*!
 * This looks for a command in the form:
 * c:<digit>,<digit>,<digit>
 * and tries to set a neopixel to that color when found
 * 
 * example:
 * c:255,128,0
 */
void parseCharacter(char character)
{
    switch (state) {
    case SerialState::WaitingStart:
        if (character == 'c') {
            state = SerialState::GotC;
        }
        break;
    case SerialState::GotC:
        if (character == ':') {
            state = SerialState::ParsingRed;
            red = 0;
        } else {
            state = SerialState::WaitingStart;
        }
        break;
    case SerialState::ParsingRed:
        if (isDigit(character)) {
            red = (red * 10) + asciiToIntValue(character);
        } else if (character == ',') {
            state = SerialState::ParsingGreen;
            green = 0;
        } else {
            state = SerialState::WaitingStart;
        }
        break;
    case SerialState::ParsingGreen:
        if (isDigit(character)) {
            green = (green * 10) + asciiToIntValue(character);
        } else if (character == ',') {
            state = SerialState::ParsingBlue;
            blue = 0;
        } else {
            state = SerialState::WaitingStart;
        }
        break;
    case SerialState::ParsingBlue:
        if (isDigit(character)) {
            blue = (blue * 10) + asciiToIntValue(character);
            break;
        }
        pixel.setPixelColor(0, pixel.Color(red, green, blue));
        pixel.show();
        state = SerialState::WaitingStart;
        break;
    }
}
