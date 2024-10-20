cd ..\src
docker build -t dataaccess .
cd ..\test

docker run -v C:/Users/tcvin/.aws:/root/.aws -p 9000:8080 dataaccess
