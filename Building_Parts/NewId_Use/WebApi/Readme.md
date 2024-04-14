# Generate Sortable Unique IDs With the NewId Library

- [Source](https://code-maze.com/dotnet-generate-sortable-unique-ids-with-the-newid-library/)

This Project uses `NewId` library and generate unique ids which is sortable can be used to retrive data from the database.

## Why Might We Need Sortable Unique IDs in .NET

We all know there are two main approaches when it comes to generating IDs for entities in our projects: either an int or Guid (globally unique identifier) values. But both approaches have their problems.

## What is NewId

**The NewId library is a NuGet package that we can use to generate unique, yet sortable IDs**. It is based on the now-retired Snowflake: an internal service at X (formerly Twitter) for generating sortable unique primary keys. NewId is part of the distributed application framework `MassTransit` and was developed to tackle problems present with both `int` and `Guid` identifiers. It’s aimed at providing a way to generate unique and sortable IDs at scale.