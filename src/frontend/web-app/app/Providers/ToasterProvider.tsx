'use client'
import { Slide, ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default function ToasterProvider() {
    return (
        <ToastContainer position='bottom-right' transition={Slide}/>
    )
}