import sys
from PIL import Image

# src: https://gist.github.com/odyniec/3470977
def autocrop_image(image, border = 0):

    black = Image.new('RGBA', image.size)
    image = Image.composite(image, black, image)

    # Get the bounding box
    bbox = image.getbbox()

    # Crop the image to the contents of the bounding box
    image = image.crop(bbox)

    # Determine the width and height of the cropped image
    (width, height) = image.size

    # Add border
    width += border * 2
    height += border * 2
    
    # Create a new image object for the output image
    cropped_image = Image.new("RGBA", (width, height), (0,0,0,0))

    # Paste the cropped image onto the new image
    cropped_image.paste(image, (border, border))

    # Done!
    return cropped_image

def end(msg=''):
    print(msg)
    input()
    exit(0)
    
files = sys.argv[1:]
#files = ['collectible_donut_00.png']
if len(files) == 0:
    end ("No files to edit.")

print (files)

for file in files:
    im = Image.open(file)
    im = autocrop_image(im)
    im.save(file)

# wait for enter, otherwise we'll just close on exit
end('AAAAAA')