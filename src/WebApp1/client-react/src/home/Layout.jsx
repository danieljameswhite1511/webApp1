import {Outlet} from "react-router-dom";
import Header from "./Header.jsx";
import Footer from "./Footer.jsx";
import "./Layout.scss"
export default function Layout() {
    return (
        <>
    <div className="layout">
        <Header />
        <div className="main">
            <Outlet />
        </div>
        <Footer />
    </div>
        </>
    )
}