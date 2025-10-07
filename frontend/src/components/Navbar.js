import React from 'react';
import { Link } from 'react-router-dom';
export default function Navbar(){ return (
  <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
    <div className="container-fluid">
      <Link className="navbar-brand" to="/">ProductCatalog</Link>
      <div className="collapse navbar-collapse">
        <ul className="navbar-nav me-auto">
          <li className="nav-item"><Link className="nav-link" to="/products">Products</Link></li>
          <li className="nav-item"><Link className="nav-link" to="/categories">Categories</Link></li>
        </ul>
        <Link className="btn btn-outline-light" to="/products/new">Add Product</Link>
      </div>
    </div>
  </nav>
); }
