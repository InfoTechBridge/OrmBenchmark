# OrmBenchmark
[![License](http://img.shields.io/:license-MIT-blue.svg)](https://raw.githubusercontent.com/giacomelli/JobSharp/master/LICENSE)

ORM Benchmarking

The goal of project is to show how long it takes to execute SELECT statement(s) against a MS SQL Server database and map the returned data to POCO/Dynamic objects in different ORMs.

The performance tests are broken in the following lists:

- Performance test of executing one sql command repetedly:
	- Performance of execute one select statement and map a returned row to a POCO object over 500 iterations
	- Performance of execute one select statement and map a returned row to a dynamic object over 500 iterations

- Performance test of mapping database records to POCO/Dynamic objects:
	- Performance of mapping 5000 rows returned by one SELECT to 5000 POCO objects in one iteration
	- Performance of mapping 5000 rows returned by one SELECT to 5000 Dynamic objects in one iteration


### How to run the benchmarks ###

Just download the project and run it or add your favorit ORM as a plugin to the project.