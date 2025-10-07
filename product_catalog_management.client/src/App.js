// src/App.js
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import ProductsPage from "./pages/ProductsPage";
import ProductFormPage from "./pages/ProductFormPage";
import CategoriesPage from "./pages/CategoriesPage";

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={<ProductsPage />} />
        <Route path="/products" element={<ProductsPage />} />
        <Route path="/products/create" element={<ProductFormPage />} />
        <Route path="/products/edit/:id" element={<ProductFormPage />} />
        <Route path="/categories" element={<CategoriesPage />} />
      </Routes>
    </Router>
  );
}

export default App;
