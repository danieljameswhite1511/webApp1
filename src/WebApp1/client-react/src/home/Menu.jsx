import {MdMenu} from "react-icons/md";
import "./Menu.scss"
import {Link} from "react-router-dom";
import {useState} from "react";

export default function Menu() {
    
    let [open, setOpen] = useState(false);
    function openMenu() {
        setOpen(!open);
    }
    return (
        <>
            <div className="menu-container">
                <div className="menu">
                    <div className="menu-icon">
                        <MdMenu onClick={openMenu}></MdMenu>
                    </div>
                    <div className={`menu-body ${open ? "open" : 'closed'}`} >
                        <Link className="menu-item"  to="/">Home</Link>
                        <Link className="menu-item" to="/about">About</Link>
                        <Link className="menu-item" to="/register">Register</Link>
                    </div>
                </div>
            </div>
        </>
    )
}