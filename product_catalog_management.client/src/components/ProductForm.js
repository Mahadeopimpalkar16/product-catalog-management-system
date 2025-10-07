import { useEffect, useState } from "react";
import api from "../api/api";
import {
  Box,
  Button,
  TextField,
  MenuItem,
  FormControl,
  InputLabel,
  Select,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";

export default function ProductForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [open, setOpen] = useState(true);
  const [categories, setCategories] = useState([]);
  const [product, setProduct] = useState({
    name: "",
    description: "",
    price: "",
    categoryId: "",
  });

  useEffect(() => {
    fetchCategories();
    if (id) fetchProduct();
  }, [id]);

  const fetchCategories = async () => {
    const res = await api.get("/categories");
    setCategories(res.data);
  };

  const fetchProduct = async () => {
    const res = await api.get(`/products/${id}`);
    setProduct({ ...res.data, categoryId: res.data.categoryId });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!product.name || !product.price || !product.categoryId)
      return alert("Please fill all required fields");

    if (id)
      await api.put(`/products/${id}`, { ...product, id: Number(id) });
    else await api.post("/products", product);

    handleClose();
  };

  const handleClose = () => {
    setOpen(false);
    navigate("/products");
  };

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="sm"
      fullWidth
      PaperProps={{
        sx: {
          borderRadius: 3,
          p: 2,
        },
      }}
    >
      <DialogTitle sx={{ textAlign: "center", fontWeight: "bold" }}>
        {id ? "Edit Product" : "Add New Product"}
      </DialogTitle>

      <DialogContent dividers>
        <Box
          component="form"
          onSubmit={handleSubmit}
          sx={{
            display: "flex",
            flexDirection: "column",
            gap: 2,
            mt: 1,
          }}
        >
          <TextField
            label="Name"
            value={product.name}
            onChange={(e) => setProduct({ ...product, name: e.target.value })}
            required
          />
          <TextField
            label="Description"
            multiline
            rows={3}
            value={product.description}
            onChange={(e) =>
              setProduct({ ...product, description: e.target.value })
            }
          />
          <TextField
            type="number"
            label="Price"
            value={product.price}
            onChange={(e) =>
              setProduct({ ...product, price: Number(e.target.value) })
            }
            required
          />
          <FormControl required>
            <InputLabel>Category</InputLabel>
            <Select
              value={product.categoryId}
              label="Category"
              onChange={(e) =>
                setProduct({ ...product, categoryId: e.target.value })
              }
            >
              {categories.map((c) => (
                <MenuItem key={c.id} value={c.id}>
                  {c.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Box>
      </DialogContent>

      <DialogActions sx={{ justifyContent: "center", gap: 2, pb: 2 }}>
        <Button onClick={handleClose} variant="outlined" color="secondary">
          Cancel
        </Button>
        <Button
          variant="contained"
          color="primary"
          onClick={handleSubmit}
          type="submit"
        >
          {id ? "Update" : "Add"} Product
        </Button>
      </DialogActions>
    </Dialog>
  );
}
