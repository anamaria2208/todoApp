import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import { HomeSharp } from "@mui/icons-material";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../redux/store";
import { logoutSuccess } from "../redux/features/authSlice";

export default function Navbar() {
  const isLoggedIn = useSelector((state: RootState) => state.auth.isLoggedIn);
  const dispatch = useDispatch();

  const handleLogout = () => {
    dispatch(logoutSuccess())
    sessionStorage.removeItemItem("jwtToken");
  }

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          ></IconButton>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            <Button color="inherit" href="/" startIcon={<HomeSharp />}>
              TODO APP
            </Button>
          </Typography>
          {isLoggedIn ? (
            <Button  color="inherit" onClick={handleLogout}>
              LOGOUT
            </Button>
          ) : (
            <Button href="/login" color="inherit">
              LOGIN
            </Button>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
}
