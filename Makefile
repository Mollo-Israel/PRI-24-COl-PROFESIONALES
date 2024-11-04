
build:
	' nombre del usurario/proyectocolprofesionales .
	docker build -t kevineduardoalanesortega/proyectocolprofesionales .

tag:
	docker tag proyectocolprofesionales kevineduardoalanesortega/proyectocolprofesionales:latest

login:
	docker login

push:
	docker push kevineduardoalanesortega/proyectocolprofesionales:latest

run:
	docker run -d -p 8080:80 --name my_server kevineduardoalanesortega/proyectocolprofesionales:latest
	