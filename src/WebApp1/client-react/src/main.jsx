import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import * as mdIcons from 'react-icons/md';
import App from './App.jsx'
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import About from "./home/About.jsx";
import NotFoundPage from "./home/NotFoundPage.jsx";
import Register from "./auth/Register.jsx";
import Layout from "./home/Layout.jsx";

const router = createBrowserRouter([
    {path:'/', element: <Layout/>, 
        children : [
            {index: true, element: <App/>},
            {path:"/about",element: <About/>},
            {path: "/register",element: <Register/>},
            {path: "*", element: <NotFoundPage/>}
        ]},
]);
    
createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router}/>
  </StrictMode>,
)
