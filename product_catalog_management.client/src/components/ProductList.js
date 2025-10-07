import { useEffect, useState } from "react";
import api from "../api/api";
import { DataGrid } from "@mui/x-data-grid";
import { Box, Button, TextField, MenuItem } from "@mui/material";
import { useNavigate } from "react-router-dom";

export default function ProductList() {
  const [products, setProducts] = useState([]);
  const [search, setSearch] = useState("");
  const [categories, setCategories] = useState([]);
  const [categoryFilter, setCategoryFilter] = useState("");
  const [sortBy, setSortBy] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    fetchCategories();
    fetchProducts();
  }, [search, categoryFilter, sortBy]);

  const fetchProducts = async () => {
    const res = await api.get("/products");
    let data = res.data;

    if (search.trim() !== "") {
      data = data.filter((p) =>
        p.name.toLowerCase().includes(search.toLowerCase())
      );
    }

    if (categoryFilter) {
      data = data.filter((p) => p.categoryId === parseInt(categoryFilter));
    }

    if (sortBy === "name") {
      data.sort((a, b) => a.name.localeCompare(b.name));
    } else if (sortBy === "price") {
      data.sort((a, b) => a.price - b.price);
    }

    setProducts(
      data.map((p) => ({
        id: p.id,
        name: p.name,
        description: p.description,
        price: Number(p.price),
        categoryName: p.categoryName,
        createdDate: p.createdDate,
      }))
    );
  };

  const fetchCategories = async () => {
    const res = await api.get("/categories");
    setCategories(res.data);
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this product?")) {
      await api.delete(`/products/${id}`);
      fetchProducts();
    }
  };

  const columns = [
    { field: "name", headerName: "Product Name", flex: 1.5, headerClassName: "bold-header" },
    { field: "description", headerName: "Description", flex: 2, headerClassName: "bold-header" },
    { 
      field: "price", 
      headerName: "Price (₹)", 
      flex: 1, 
      headerClassName: "bold-header", 
    },
    { field: "categoryName", headerName: "Category", flex: 1, headerClassName: "bold-header" },
    { 
      field: "createdDate", 
      headerName: "Created On", 
      flex: 1, 
      headerClassName: "bold-header", 
    },
    {
      field: "actions",
      headerName: "Actions",
      flex: 1,
      sortable: false,
      headerClassName: "bold-header",
      renderCell: (params) => (
        <>
          <Button variant="contained" color="warning" size="small" sx={{ mr: 1 }} onClick={() => navigate(`/products/edit/${params.row.id}`)}>Edit</Button>
          <Button variant="contained" color="error" size="small" onClick={() => handleDelete(params.row.id)}>Delete</Button>
        </>
      ),
    },
  ];

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ display: "flex", justifyContent: "space-between", flexWrap: "wrap", alignItems: "center", mb: 2, gap: 2 }}>
        <Box sx={{ display: "flex", gap: 2 }}>
          <Button variant="contained" color="primary" onClick={() => navigate("/products/create")}>+ Add Product</Button>

          <TextField select label="Filter by Category" value={categoryFilter} onChange={(e) => setCategoryFilter(e.target.value)} size="small" sx={{ width: 200 }}>
            <MenuItem value="">All</MenuItem>
            {categories.map((c) => <MenuItem key={c.id} value={c.id}>{c.name}</MenuItem>)}
          </TextField>

          <TextField select label="Sort by" value={sortBy} onChange={(e) => setSortBy(e.target.value)} size="small" sx={{ width: 180 }}>
            <MenuItem value="">None</MenuItem>
            <MenuItem value="name">Name</MenuItem>
            <MenuItem value="price">Price</MenuItem>
          </TextField>
        </Box>

        <TextField label="Search by name..." variant="outlined" size="small" sx={{ width: 250 }} value={search} onChange={(e) => setSearch(e.target.value)} />
      </Box>

      <Box sx={{ height: 600, width: "100%", "& .MuiDataGrid-row:nth-of-type(odd)": { backgroundColor: "#f9f9f9" }, "& .MuiDataGrid-columnHeaders": { backgroundColor: "#e0e0e0", fontWeight: "bold", fontSize: "1.1rem" } }}>
        <DataGrid rows={products} columns={columns} pageSize={10} rowsPerPageOptions={[5, 10, 20]} disableSelectionOnClick sx={{ borderRadius: 2, boxShadow: 3 }} />
      </Box>
    </Box>
  );
}
