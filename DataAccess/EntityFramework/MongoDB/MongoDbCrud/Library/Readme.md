# MongoDb Crud Operations

This Class Library Project implements MongoDB Crud operations using `BSONDocument (Unstructured)`, `C# class (Structure)` approach.


## To Create Document
- Using `InsertOne` and `InsertMany` method to create new document(s) with `BSON and C# class` way.
- Using `InsertOneAsync` and `InsertManyAsync` asynchronous method to create new documents with `BSON and C# class` way.


## To Read Document
- Using `Find` method to retrive the single or all data with `BSON and C# class` way.
- Using `FindAsync` asynchronous method to retrive the single or all data with `BSON and C# class` way.
- Using `LINQ and Builders` class to filter out and retrive document(s).


## To Update Document
- Using `UpdateOne` and `UpdateMany` method to create new document(s) with `BSON and C# class` way.
- Using `UpdateOneAsync` and `UpdateManyAsync` asynchronous method to create new document(s) with `BSON and C# class` way.
- Using `LINQ and Builders` class to filter out and update the document(s).


## To Delete Document
- Using `DeleteOne` and `DeleteMany` method to Delete new document(s) with `BSON and C# class` way.
- Using `DeleteOneAsync` and `DeleteManyAsync` asynchronous method to Delete new document(s) with `BSON and C# class` way.
- Using `LINQ and Builders` class to filter out and Delete the document(s).


## MongoDB Multi-Document Transaction
- Used MongoDB Transaction to implement a unit of work i.e. multiple database operations as a single work.