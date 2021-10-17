# MmtTest
This repository contains an implementation for the technical test for the company MMT

# How to Use it

To Use it you'll need the follwoing : 
  * .Net Core 3.1 SDK
  * Visual Studio 2019


After runned using IIExpress Go to ./swagger to test the api

# Considerations

Before be promoted to production I would suggest the following 
  * Use a system parameter storage (i.e AWS) to avoid sensible data (apikey , connections strings ) are hardcoded in the source control
  * Use stored procedures to manage Relation DB entities
  * Add Integration Tests
  * Integrate with a cloud monitoring infrastructure (i.e datadog) 
  * Dockerize the application 
  * Use automation server that runs tests for each commit





