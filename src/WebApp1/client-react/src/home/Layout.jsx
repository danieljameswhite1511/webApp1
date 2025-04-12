import {Outlet} from "react-router-dom";
import Header from "./Header.jsx";
import Footer from "./Footer.jsx";
import "./Layout.scss"
import Menu from "./Menu.jsx";
export default function Layout() {
    return (
        <>
            <div className="layout-container">
                <div className="layout">
                    <Header />
                    <div className="main">
                        <Outlet />

                    </div>
                    <Footer />

                </div>
                <Menu></Menu>
            </div>
    
        </>
    )
}