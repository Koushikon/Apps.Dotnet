# Basic Authentication using Http Authorization header.

This Server project demonstrate Basic Authentication technique using Http Authorization header. `Base-64` is a very simple algorithm with no encryption which is easy to reverse But knowing this may help in some situation.

Example - `Authorization: Basic QWRtaW46MTIzNA==`

- [Base64 Encode Site](https://www.base64encode.org/)

## To Test out the Api:
1. Open any Api Testing Tool.
2. Enter the request url.
3. Set the Http method to GET.
4. Inside Header section add a new Header.
5. With name "Authorization" and value "Basic QWRtaW46MTIzNA==".
6. Now, Run the request and get the Weather results.
7. If we didn’t provide the Authorization header or provide wrong one, we’ll get 401 error.