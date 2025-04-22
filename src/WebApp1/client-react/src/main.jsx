import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import About from "./home/About.jsx";
import NotFoundPage from "./home/NotFoundPage.jsx";
import Layout from "./home/Layout.jsx";
import Register from "./auth/Register.jsx";
import {QueryClient, QueryClientProvider} from "react-query";
import CheckEmail from "./auth/CheckEmail.jsx";
import ConfirmEmail from "./auth/ConfirmEmail.jsx";
import Login from "./auth/login.jsx";
import {AuthProvider} from "./auth/providers/AuthProvider.jsx";

const queryClient = new QueryClient();
const router = createBrowserRouter([
    {path:'/', element: <Layout/>, 
        children : [
            {index: true, element: <App/>},
            {path:"/about", element: <About/>},
            {path: "/register", element: <Register/>},
            {path: "/check-email", element: <CheckEmail/>},
            {path: "/confirm-email", element: <ConfirmEmail/>},
            {path: "/login", element: <Login/>},
            {path: "*", element: <NotFoundPage/>}
        ]},
]);
    
createRoot(document.getElementById('root')).render(
  /*<StrictMode>*/
    <AuthProvider>
        <QueryClientProvider client={queryClient}>
            <RouterProvider router={router}/>
        </QueryClientProvider>
    </AuthProvider>
  /*</StrictMode>,*/
)
