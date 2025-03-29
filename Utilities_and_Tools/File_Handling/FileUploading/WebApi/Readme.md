# Large Uile Uploading in Asp.Net Core Web Api

Rather than using `byte[]` or `MemoryStream` when handling large files we're using `Stream` which are generally more faster.

## Test the Api:

1. Open any Api Tesing Tool like `Thunder Client`.
2. Click `New Request`.
3. Enter the upload request url.
4. Change the Request Type to `POST`.
5. Then in the `Body` section under `Form`.
6. Turn On Files Checkbox.
7. Now click on `choose file` and enter `field name` as the name.6
8. (Optional) Add more files this way.
9. Now click on `Send`.
10. Response type `201`: Created means added successfully or `415`: Unsupported Media Type means uploaded files not supported by the api.

