import {useNavigate, useSearchParams} from "react-router-dom";
import authService from "./services/auth.service.js";
import {useEffect, useState} from "react";
import Card from "../Card/Card.jsx";

export default  function ConfirmEmail() {
    const [searchParams] = useSearchParams();
    const userId = searchParams.get("userId");
    const token = searchParams.get("token");
    const[emailConfirmed, setEmailConfirmed] = useState(false);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
   
    useEffect( () => {
        async function verifyEmail()
        {
            if (loading) return;
            setLoading(true);
            authService.confirmEmail(userId, token).then((response) => {
                console.log(response.data);
                setEmailConfirmed(response.data)
                if(response.data){
                    
                    const timeoutId = setTimeout(()=>{
                        navigate("/login");
                        clearTimeout(timeoutId);
                    }, 3000)
                    
                    
                    
                }else{
                    console.log(response);
                }
                
            }).catch((error) => {
                console.log(error);
            }).finally(x => {
            });
        }
        verifyEmail();
    }, []);

        
    return (
        
        <div>
            
            { (
                <Card>
                    {emailConfirmed ? "Email Confirmed" : "Email not confirmed"}
                </Card>
            )}
        </div>
    )
}