import { Container, TextField, Typography, Divider } from '@mui/material';
import { useEffect, useState } from 'react';
import CssBaseline from '@mui/material/CssBaseline';

function App() {
  debugger
  const [todo, setTodo] = useState([])

  const fetchData = async () => {
    const response = await fetch("https://localhost:5001/api/todoitem/")
    const data = await response.json();
    console.log("TODO" ,data)
    setTodo(data)
  }

  useEffect(() => {
    fetchData()
  }, [])
  

  return (
    <>
    <CssBaseline />
    <Container maxWidth="sm">
    <Typography variant="h2" align='center'>Todo App</Typography>
    <Divider />
     <TextField  label="Enter todo ..." variant="outlined" id="fullWidth"/>
    </Container>
    
    </>
  )
}

export default App
