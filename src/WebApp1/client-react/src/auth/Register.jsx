
import Button from "../form-controls/Button.jsx";
import "./Register.scss"
import Card from "../Card/Card.jsx";
import {useForm} from "react-hook-form"
import {yupResolver} from "@hookform/resolvers/yup"
import authService from "./services/auth.service.js"
import * as schemas from "../form-validation/schemas.js"
import {useState} from "react";
import {useNavigate} from "react-router-dom"   

export default function Register() {
    const navigate = useNavigate();
    const [serverErrors, setServerErrors] = useState([]);
    
        const {
            register,
            handleSubmit,
            formState: { errors , isSubmitting},
            reset,
            getValues,
        } = useForm({
            resolver: yupResolver(schemas.registerSchema),
        });

       async function onSubmit(data) {
           setServerErrors([]);
           await authService.register(data.email, data.password, data.confirmPassword)
               .then((response) => {
                   reset();
                   navigate("/check-email");
                   
               }).catch((errors) => {
                   setServerErrors(errors);
               });
        }

    return (
         <>
             <Card>
                 <div className="m-5">
                     <div className="p-5" style={{ width: '400px' }}>
                         <form onSubmit={handleSubmit(onSubmit)}>
                             <div className="flex justify-between">
                                 <label htmlFor="email">Email</label>
                                 <input {...register("email")}
                                        className=" border  border-green-950"
                                        placeholder="Enter Email">
                                 </input>
                             </div>
                             <div className="flex justify-between">
                                 <label htmlFor="password">Password</label>
                                 <input {...register("password")}
                                        className=" border  border-green-950"
                                        placeholder="Enter Password"/>
                             </div>

                             <div className="flex justify-between">
                                 <label htmlFor="confirmPassword">Confirm Password</label>
                                 <input {...register("confirmPassword")}
                                        className=" border  border-green-950"
                                        placeholder="Confirm Password"/>
                             </div>
                             <div className="flex justify-end">
                                 <Button label="Register"
                                         disabled={isSubmitting}
                                         onClick={handleSubmit(onSubmit)}>
                                 </Button>
                             </div>
                         </form>
                         <div className="client-side-errors">
                             {errors.email && (
                                 <div>{...errors.email.message}</div>
                             )}
                             {errors.password && (
                                 <div>{...errors.password.message}</div>
                             )}
                             {errors.confirmPassword && (
                                 <div>{...errors.confirmPassword.message}</div>
                             )}    
                         </div>
                         <div className="server-side-errors">
                             {serverErrors.length > 0 && 
                                 serverErrors.map((error, index) => (
                                     <div key={index}> 
                                         {error}
                                     </div>
                                 ))}
                         </div>
                     </div>
                 </div>     
             </Card>
        </>
    )
}