# Generic API Abstraction Layer

The Generic API Abstraction Layer is a C# library that provides generic CRUD (Create, Read, Update, Delete) operations for working with objects in an API. It offers a class called `GenericCrudProvider<TObj, TKey>` that implements the `ICrudProvider<TObj, TKey>` interface.

## Table of Contents

- [Dependencies](#dependencies)
- [Installation](#installation)
- [Usage](#usage)
- [API Reference](#api-reference)
- [Contributing](#contributing)
- [License](#license)

## Dependencies

The Generic API Abstraction Layer has the following dependencies:

- `GenericApiAbstractionLayer.Extensions`
- `GenericApiAbstractionLayer.Interfaces`
- `Microsoft.Extensions.Logging`
- `Newtonsoft.Json`

Make sure to install and reference these dependencies in your project.

## Installation

To use the Generic API Abstraction Layer in your project, follow these steps:

1. Add the `GenericApiAbstractionLayer` namespace to your project.
2. Install the required dependencies listed in the [Dependencies](#dependencies) section.

## Usage

To use the Generic API Abstraction Layer, you need to follow these steps:

1. Create an instance of the `HttpClient` class to make API requests.
2. Instantiate the `GenericCrudProvider<TObj, TKey>` class, providing an `ILogger` instance and the `HttpClient` instance.
3. Use the methods provided by the `GenericCrudProvider<TObj, TKey>` class to perform CRUD operations on your objects.

Here's an example of how to use the Generic API Abstraction Layer:

```csharp
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using GenericApiAbstractionLayer;

// Create an instance of HttpClient
var httpClient = new HttpClient();

// Create an instance of ILogger
var logger = new Logger<GenericCrudProvider<MyObject, int>>();

// Instantiate the GenericCrudProvider
var crudProvider = new GenericCrudProvider<MyObject, int>(logger, httpClient);

// Perform CRUD operations
var objects = await crudProvider.GetAsync();
var newObject = await crudProvider.PostAsync(new MyObject());
var updatedObject = await crudProvider.PutAsync(id, new MyObject());
var deletedObject = await crudProvider.DeleteAsync(id);
