# Vue 3 + Vuetify 3 + TypeScript + .NET 8.0 Application

This project demonstrates a full-stack application using Vue 3 with Vuetify 3 for the frontend and .NET 8.0 for the backend.

## Tech Stack

- **Frontend**: Vue 3, TypeScript, Vuetify 3, Vue Router
- **Backend**: .NET 8.0 Web API
- **State Management**: Vue Composition API
- **HTTP Client**: Axios

## Project Structure

```
project-root/
├── vue-app/                 # Vue 3 frontend application
│   ├── src/
│   │   ├── components/      # Reusable Vue components
│   │   ├── views/           # Page-level components
│   │   ├── composables/     # Reusable logic
│   │   ├── router/          # Vue Router configuration
│   │   └── types/           # TypeScript type definitions
│   ├── package.json         # Frontend dependencies
│   └── vite.config.ts       # Vite build configuration
└── dotnet-api/              # .NET 8.0 backend API
    ├── Controllers/         # API controllers
    ├── Models/              # Data models
    ├── Services/            # Business logic services
    ├── Program.cs           # Application startup
    └── dotnet-api.csproj    # .NET project file
```

## Setup Instructions

### Prerequisites

- Node.js 18+ (for Vue frontend)
- .NET 8.0 SDK (for backend API)
- npm or yarn package manager

### Frontend Setup

1. Navigate to the Vue app directory:
```bash
cd vue-app
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm run dev
```

The frontend will be available at http://localhost:3000

### Backend Setup

1. Navigate to the .NET API directory:
```bash
cd dotnet-api
```

2. Restore packages:
```bash
dotnet restore
```

3. Run the API:
```bash
dotnet run
```

The API will be available at http://localhost:5000

## API Endpoints

The .NET API provides the following endpoints:

- `GET /api/test` - Test endpoint
- `GET /api/sample` - Get all samples
- `GET /api/sample/{id}` - Get sample by ID
- `POST /api/sample` - Create a new sample
- `PUT /api/sample/{id}` - Update an existing sample
- `DELETE /api/sample/{id}` - Delete a sample

## Development Features

- Hot module replacement for Vue components
- Type checking with TypeScript
- Material Design components with Vuetify
- API proxy configured in Vite
- CORS enabled for local development

## Building for Production

### Frontend

```bash
npm run build
```

### Backend

```bash
dotnet publish -c Release -o ./publish
```

## Testing API Connection

The home page includes a "Test API Connection" button that calls the `/api/test` endpoint to verify communication between the frontend and backend.

## File Structure Notes

- The Vue app proxies API requests from `/api` to the .NET backend during development
- TypeScript interfaces are used throughout for type safety
- Composables provide reusable business logic
- Vuetify components implement Material Design principles