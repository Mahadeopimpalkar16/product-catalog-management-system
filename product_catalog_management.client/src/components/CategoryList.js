import { useEffect, useState } from "react";
import api from "../api/api";
import {
  Box,
  Button,
  TextField,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Typography,
  IconButton,
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { Delete, Edit } from "@mui/icons-material";

export default function CategoryList() {
  const [categories, setCategories] = useState([]);
  const [search, setSearch] = useState("");
  const [open, setOpen] = useState(false);
  const [newCategory, setNewCategory] = useState({ id: null, name: "" });

  // ✅ Proper useEffect with async loader
  useEffect(() => {
    const loadData = async () => {
      await fetchCategories();
    };
    loadData();
  }, []);

  const fetchCategories = async () => {
    try {
      const res = await api.get("/categories");
      setCategories(res.data);
    } catch (err) {
      console.error("Error fetching categories:", err);
    }
  };

  const handleOpen = (category = null) => {
    if (category) setNewCategory({ id: category.id, name: category.name });
    else setNewCategory({ id: null, name: "" });
    setOpen(true);
  };

  const handleClose = () => setOpen(false);

  const handleSubmit = async () => {
    if (!newCategory.name.trim()) return alert("Category name is required");

    try {
      if (newCategory.id)
        await api.put(`/categories/${newCategory.id}`, newCategory);
      else await api.post("/categories", { name: newCategory.name });

      handleClose();
      fetchCategories();
    } catch (err) {
      console.error("Error saving category:", err);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this category?")) {
      try {
        await api.delete(`/categories/${id}`);
        fetchCategories();
      } catch (err) {
        console.error("Error deleting category:", err);
      }
    }
  };

  const filteredCategories = categories.filter((c) =>
    c.name.toLowerCase().includes(search.toLowerCase())
  );

  const columns = [
    { field: "id", headerName: "ID", width: 100 },
    {
      field: "name",
      headerName: "Category Name",
      flex: 1,
      headerClassName: "super-app-theme--header",
    },
    {
      field: "actions",
      headerName: "Actions",
      width: 120,
      sortable: false,
      renderCell: (params) => (
        <>
          <IconButton color="primary" onClick={() => handleOpen(params.row)}>
            <Edit />
          </IconButton>
          <IconButton color="error" onClick={() => handleDelete(params.row.id)}>
            <Delete />
          </IconButton>
        </>
      ),
    },
  ];

  return (
    <Box sx={{ p: 3 }}>
      {/* Header Row */}
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 2,
        }}
      >
        <Typography variant="h5" fontWeight="bold">
          Categories
        </Typography>
        <Box sx={{ display: "flex", gap: 2 }}>
          <TextField
            size="small"
            placeholder="Search..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />
          <Button variant="contained" onClick={() => handleOpen()}>
            Add Category
          </Button>
        </Box>
      </Box>

      {/* Table */}
      <Box
        sx={{
          height: 500,
          width: "100%",
          "& .super-app-theme--header": {
            fontWeight: "bold",
            fontSize: "1rem",
            backgroundColor: "#f3f3f3",
          },
          "& .MuiDataGrid-row:nth-of-type(even)": {
            backgroundColor: "#fafafa",
          },
        }}
      >
        <DataGrid
          rows={filteredCategories}
          columns={columns}
          pageSize={7}
          rowsPerPageOptions={[7, 14, 21]}
          disableSelectionOnClick
        />
      </Box>

      {/* Add/Edit Popup */}
      <Dialog open={open} onClose={handleClose} fullWidth maxWidth="xs">
        <DialogTitle>
          {newCategory.id ? "Edit Category" : "Add Category"}
        </DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            label="Category Name"
            fullWidth
            value={newCategory.name}
            onChange={(e) =>
              setNewCategory({ ...newCategory, name: e.target.value })
            }
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button variant="contained" onClick={handleSubmit}>
            {newCategory.id ? "Update" : "Add"}
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
