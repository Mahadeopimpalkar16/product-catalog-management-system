import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import ProductList from './pages/ProductList';
import ProductForm from './pages/ProductForm';
import CategoryList from './pages/CategoryList';
import Navbar from './components/Navbar';

export default function App(){
  return (
    <div>
      <Navbar />
      <div className="container mt-4">
        <Routes>
          <Route path="/" element={<Navigate to="/products" replace />} />
          <Route path="/products" element={<ProductList />} />
          <Route path="/products/new" element={<ProductForm />} />
          <Route path="/products/:id/edit" element={<ProductForm />} />
          <Route path="/categories" element={<CategoryList />} />
        </Routes>
      </div>
    </div>
  );
}
