# E-Commerce Onion Architecture (Reference Implementation)

## Overview
Reference implementation of an E-Commerce backend built using **Onion Architecture** in **.NET 8**.

## Architecture & Dependency Flow
- `Domain`: Pure C# core containing business entities and invariants.
- `Application`: Depends on `Domain`. Defines use cases, DTOs, and interface contracts.
- `Infrastructure`: Depends on `Application` only. Implements repositories, DB Context, and external adapters.
- `API`: Depends on `Application` and `Infrastructure` (for DI registration only).

## Build & Test
bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/ECommerce.API

