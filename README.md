# Inventory Hold Microservice

## Overview
This project implements an Inventory Hold system where items are temporarily reserved during checkout to prevent overselling.

---

## Tech Stack

### Backend
- .NET Web API
- MongoDB (atomic operations)
- Redis (caching)
- RabbitMQ (event-driven)
- Serilog (logging)

### Frontend
- React + TypeScript (Vite)
- Zustand (state management)
- Axios (API calls)

---

## Architecture

Clean Architecture with Domain-Driven Design:

src/
- Contracts
- Domain
- Infrastructure
- WebApi
- UnitTests

---

## Features

- Create inventory hold
- Release hold
- Auto-expiry of holds
- Inventory consistency
- Redis caching
- Event publishing (RabbitMQ)
- Background worker for expiry
- Unit testing (xUnit)

---

## API Endpoints

### Create Hold
POST /api/holds

### Get Hold
GET /api/holds/{id}

### Release Hold
DELETE /api/holds/{id}

### Inventory
GET /api/inventory

---

## Run Locally (Without Docker)

### Backend
dotnet run

### Frontend
cd frontend
npm install
npm run dev

---

## Run with Docker (Recommended)

docker-compose up --build

---

## Design Decisions

- MongoDB used for flexible schema and atomic updates
- Redis used for caching high-frequency reads
- RabbitMQ for event-driven communication
- Background service for hold expiry
- Zustand chosen for lightweight state management

---

## Future Improvements

- Add authentication
- Add pagination
- Use MassTransit for RabbitMQ
- Improve UI with Material UI
- Add distributed locking

---

## Author
Ajay Joshi
