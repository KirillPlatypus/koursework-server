
Тестовый сервер для курсовой работы

1. Open terminal and navigate to the root folder.
2. Run `dotnet publish --os linux --arch x64` to create a Docker image.
3. Run `docker run -d --name lection-server -p 5010:80 lection-server:1.1.0` to run a Docker container.
4. Your test server is running on `http://localhost:5010`.

## References

- Navigate to `http://localhost:5010/swagger` to see a documentation for API.

Оригинал
`https://github.com/vordig/lection-server`
