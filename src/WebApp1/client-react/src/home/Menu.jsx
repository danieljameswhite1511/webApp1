import {MdClose, MdMenu} from "react-icons/md";
import "./Menu.scss"
import {Link} from "react-router-dom";
import {useContext, useState} from "react";
import {AuthContext, AuthProvider} from "../auth/providers/AuthProvider.jsx";

export default function Menu() {
    
    let [open, setOpen] = useState(false);
    let auth = useContext(AuthContext)
    function openMenu() {
        setOpen(true);
    }
    function closeMenu() {
        setOpen(false);
    }
    return (
        <>
            <nav>
                <div className="menu-container">
                    <div className="menu">
                        <div className="menu-icon">
                            { !open &&
                                <MdMenu onClick={openMenu}></MdMenu>   
                                
                            }
                            
                        </div>
                        <div className={`menu-body ${open ? "open" : 'closed'}`} >
                            <div className="menu-icon">
                                <MdClose onClick={closeMenu}></MdClose>
                            </div>
                            <Link className="menu-item"  to="/">Home</Link>
                            <Link className="menu-item" to="/about">About</Link>
                            {
                               !auth.isAuthenticated && <Link className="menu-item" to="/register">Register</Link>    
                            }
                            {
                                !auth.isAuthenticated && <Link className="menu-item" to="/login">Login</Link>
                            }
                            
                        </div>
                    </div>
                </div>
            </nav>
        </>
    )
}