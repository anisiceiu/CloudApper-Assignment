# CloudApper-Assignment

1.You need to download multiple images asynchronously and at a
time you can download maximum MaxDownloadAtOnce number
of images. So if any download is complete, then it will move to next
download from the queue. You have to store this images in your
Server Localhost and give them unique names.

Url:https://localhost:7242/api/Image/download-images
Method: POST

2.This image name is what you have sent us the previous API.
Expectation is that you give us the base64String for that particular
image.

Url:https://localhost:7242/api/Image/get-image-by-name/3e0d6e10-7f97-43dc-8560-c5db453e079f.png
Method: GET

**Inside Postman directory you will find postman collection.

