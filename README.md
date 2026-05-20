# Product Microservice

The Product Microservice is a highly scalable, independent backend service responsible for managing product catalogs, inventory, and categorization in an e-commerce ecosystem. Built using domain-driven design principles.



## 🚀 Features

- **CRUD Operations**: Full Create, Read, Update, and Delete capabilities for products.
- **Inventory Management**: Real-time stock tracking and updates.
- **Categorization**: Hierarchical product categories and tagging.
- **Event-Driven Architecture**: Publishes and consumes domain events (e.g., `ProductPriceChanged`, `InventoryLow`) via message brokers.
- **Caching**: Redis-backed caching for rapid retrieval of top-selling items.

## 🛠️ Technology Stack

- **Language/Framework**:
- **Database**: 
- **Caching**: [Redis](https://redis.io)
- **Message Broker**: [RabbitMQ](https://rabbitmq.com) / [Apache Kafka](https://apache.org)
- **API Documentation**: [Swagger UI](https://swagger.io)
 

## ⚙️ Prerequisites & Setup

Ensure the following tools are installed on your local machine:
- Docker & Docker Compose
- Node.js / Java JDK 17+
- npm / Maven

### Local Installation Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/DigvijayKamble/ProductService.git
   cd product-service
   ```

2. **Configure Environment Variables:**
   Create a `.env` file in the root directory based on `.env.example`:
   ```bash
   cp .env.example .env
   ```

3. **Start Dependencies:**
   Spin up local databases and message brokers via Docker:
   ```bash
   docker-compose up -d
   ```

4. **Install Dependencies & Run:**
 

## 🔌 API Endpoints

Comprehensive OpenAPI documentation is available via Swagger at `http://localhost:8080/api/v1/products/docs` when the service is running.

### Core Endpoints:
- `GET /api/v1/products` - Get a paginated list of all products.
- `GET /api/v1/products/{id}` - Get product details by ID.
- `POST /api/v1/products` - Create a new product.
- `PUT /api/v1/products/{id}` - Update an existing product.
- `DELETE /api/v1/products/{id}` - Soft-delete a product.
- `PATCH /api/v1/products/{id}/inventory` - Update product stock quantity.

## 📦 Docker & Deployment

Build the containerized image locally:
```bash
docker build -t product-service:latest .
docker run -d -p 8080:8080 --env-file .env product-service:latest
```

For production, this service is designed to be deployed on an [Amazon EKS](https://amazon.com) or [Google Kubernetes Engine (GKE)](https://google.com) cluster via [Helm](https://helm.sh).

## 🧪 Testing

Run the test suite to verify behavior:
```bash
# Unit Tests
npm run test

# Integration Tests
npm run test:e2e
```

## 🤝 Contributing

Contributions are welcome! Please read the [Contributing Guidelines](https://github.com) before submitting a pull request.

## 📜 License

This project is licensed under the [MIT License](https://opensource.org).
