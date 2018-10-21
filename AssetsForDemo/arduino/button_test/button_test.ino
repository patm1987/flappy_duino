#include <Adafruit_NeoPixel.h>

const int analogReadPin = 0;

const int pixelPin = 6;
const int ledPin = 13;

Adafruit_NeoPixel pixel = Adafruit_NeoPixel(1, pixelPin, NEO_GRB + NEO_KHZ800);

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
    Serial.print("Reading = ");
    Serial.println(reading);

    pixel.setPixelColor(0, pixel.Color(reading/4, 255-reading/4, 0));
    pixel.show();
}
