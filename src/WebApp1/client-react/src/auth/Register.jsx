
import Button from "../form-controls/Button.jsx";
import Input from "../form-controls/Input.jsx";
import "./Register.scss"
import Card from "../Card/Card.jsx";
import {useForm} from "react-hook-form"
import * as yup from "yup";
import {yupResolver} from "@hookform/resolvers/yup"

export default function Register() {
    
    const schema = yup.object(
        {
            email: yup.string().email().required("Email is required"),
            password: yup.string().min(8).required("Password is required"),
            confirmPassword: yup.string().min(8)
                .required("Confirm passwords is required")
                .test("passwordsMatch", "Passwords must match",
                    function(value) {
                        return this.resolve(getValues("password") === value);
                    }),
        }
    )
    
    const {
        register,
        handleSubmit,
        formState: { errors , isSubmitting},
        reset,
        getValues,
    } = useForm({
        resolver: yupResolver(schema),
    });

   async function onSubmit(data) {
        console.log(data);
        
        await new Promise(resolve => setTimeout(resolve, 1000));
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
                 </div>     
             </Card>
            
        </>
    )
}