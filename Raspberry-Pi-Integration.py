# imports
from sense_hat import SenseHat
from time import sleep
import requests

# instansiate our sense hat object
sense = SenseHat()

# clears the LEDs
sense.clear(255,255,255)

# colours
red = (255, 0, 0)
green = (0, 255, 0)
blue = (0, 0, 255)

off = (0, 0, 0)

# zones for fields
fields = [[(0, 0), (1, 0), (2, 0),(0, 1), (1, 1), (2, 1)],
[(5, 0), (6, 0), (7, 0),(5, 1), (6, 1), (7, 1)],
[(0, 3), (1, 3), (2, 3),(0, 4), (1, 4), (2, 4)],
[(5, 3), (6, 3), (7, 3),(5, 4), (6, 4), (7, 4)],
[(0, 6), (1, 6), (2, 6),(0, 7), (1, 7), (2, 7)],
[(5, 6), (6, 6), (7, 6),(5, 7), (6, 7), (7, 7)]
]


# setting all fields to off
for field in fields:
    for x, y in field:
        sense.set_pixel(x, y, off)


# sets a "healthy" field green
def healthy(field):
    for x, y in fields[field]:
        sense.set_pixel(x, y, green)


# sets a "sick" field red
def sick(field):
    for x, y in fields[field]:
        sense.set_pixel(x, y, red)


# turns an unused field off
def empty(field):
    for x, y in fields[field]:
        sense.set_pixel(x, y, off)


if __name__ == '__main__':
    while True:
        # get data from website and format it
        data = requests.get('https://hacked-zg4z.onrender.com/?connection=getc').text
        data = data.replace("[", "")
        data = data.replace("]", "")
        data = data.split(",")
        print(data)
        # go through and apply colour to each field state
        for position in range(0,6):
            value = int(data[position])
            #print(value)
            if value == 0:
                empty(position)
            elif value == 255:
                healthy(position)
            else:
                sick(position)
        sleep(0.5)
