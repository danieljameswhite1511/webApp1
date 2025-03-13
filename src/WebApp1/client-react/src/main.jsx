import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import About from "./home/About.jsx";
import NotFoundPage from "./home/NotFoundPage.jsx";

const router = createBrowserRouter([
    {path:"/",element: <App/>},
    {path:"/about",element: <About/>},
    {path: "*", element: <NotFoundPage/>}
    
]);
    
createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router}/>
  </StrictMode>,
)
