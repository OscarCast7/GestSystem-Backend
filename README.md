# 📦 Inventory & Stock Alert API (.NET 8)

API RESTful desarrollada en **C# y .NET 8** para la gestión centralizada de inventarios, control de stock y generación de alertas preventivas de reabastecimiento en tiempo real.

---

## 🚀 Tecnologías Utilizadas

* **Lenguaje:** C# (.NET 8 Web API)
* **ORM:** Entity Framework Core 9 (Code-First)
* **Base de Datos:** SQLite (`inventory.db`)
* **Documentación & Pruebas:** Swagger UI / OpenAPI
* **Consultas:** LINQ (Filtros dinámicos y agregaciones)

---

## ✨ Características Principales

* **Gestión de Productos y Categorías:** Relación $1:N$ entre categorías de infraestructura/periféricos y productos.
* **Control de Stock y Umbrales:** Detección de productos cuyo inventario actual está por debajo de su umbral mínimo configurado.
* **Filtrado Avanzado con LINQ:** Búsqueda flexible por texto (Nombre/SKU), ID de categoría o estado de stock.
* **Arquitectura DTO:** Separación estricta entre entidades de dominio y contratos de transporte de datos.

---

## 🛠️ Endpoints Principales

### 📦 Productos (`/api/products`)
* `GET /api/products` — Obtiene el catálogo de productos con parámetros de búsqueda (`search`, `categoryId`, `lowStockOnly`).
* `GET /api/products/low-stock` — Endpoint optimizado para paneles de control (Dashboard) que retorna productos con alerta de stock bajo.
* `POST /api/products` — Registra un nuevo producto con validación de SKU único.

---

## 🗄️ Modelo de Datos (SQLite)

```text
  +------------------+          +------------------+
  |    Category      |          |     Product      |
  +------------------+          +------------------+
  | PK  Id           |<--------+| PK  Id           |
  |     Name         |        1 |     SKU (Único)  |
  |     Description  |          |     Name         |
  +------------------+          |     Price        |
                                |     StockQuantity|
                                |     MinimumStock |
                                | FK  CategoryId   |
                                +------------------+
