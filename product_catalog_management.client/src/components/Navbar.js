import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <AppBar position="static" color="primary" sx={{ mb: 3 }}>
      <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
        {/* Left - Brand Name */}
        <Typography
          variant="h6"
          component={Link}
          to="/"
          sx={{
            color: "white",
            textDecoration: "none",
            fontWeight: "bold",
            letterSpacing: "0.5px"
          }}
        >
          Product Catalog
        </Typography>

        {/* Right - Nav Links */}
        <Box>
          <Button
            component={Link}
            to="/products"
            color="inherit"
            sx={{ textTransform: "none", fontWeight: 500 }}
          >
            Products
          </Button>
          <Button
            component={Link}
            to="/categories"
            color="inherit"
            sx={{ textTransform: "none", fontWeight: 500 }}
          >
            Categories
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
}
