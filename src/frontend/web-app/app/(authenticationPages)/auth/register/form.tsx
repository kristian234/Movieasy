'use client'

import { registerUser } from "@/app/actions/auth-actions";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useForm } from "react-hook-form";

interface IFormInput {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
}

const schema = yup.object().shape({
    firstName: yup.string().min(3).required(),
    lastName: yup.string().min(3).required(),
    email: yup.string().email().required(),
    password: yup.string().min(5).required(),
    confirmPassword: yup.string().oneOf([yup.ref('password')], 'Passwords must match').required(),
});

export default function RegisterForm() {
    const router = useRouter();
    const { register, handleSubmit, formState: { errors } } = useForm<IFormInput>({
        resolver: yupResolver(schema)
    });

    const onSubmit = async (data: IFormInput) => {
        registerUser(data.firstName, data.lastName, data.email, data.password).then(res => {
            console.log(data);
            console.log(res);
            if (!res.error) {
                router.push('/auth/login');
                router.refresh();
            }
        });
    };

    return (
        <div className="flex justify-center p-6 mt-10">
            <div className="w-full bg-header rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0">
                <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                    <h1 className="text-xl font-bold leading-tight tracking-tight text-secondary md:text-2xl">
                        Create your Account
                    </h1>
                    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 md:space-y-6" action="#">
                        <div className="flex justify-between">
                            <div>
                                <label htmlFor="firstName" className="block mb-2 text-sm font-bold text-third">First Name</label>
                                <input {...register('firstName')} type="text" name="firstName" id="firstName" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " placeholder="Jonathan" />
                                <p>{errors.firstName?.message}</p>
                            </div>
                            <div className="ml-1">
                                <label htmlFor="lastName" className="block mb-2 text-sm font-bold text-third">Last Name</label>
                                <input {...register('lastName')} type="text" name="lastName" id="lastName" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " placeholder="Dale" />
                                <p>{errors.lastName?.message}</p>
                            </div>
                        </div>
                        <div>
                            <label htmlFor="email" className="block mb-2 text-sm font-bold text-third">Your email</label>
                            <input {...register('email')} type="email" name="email" id="email" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " placeholder="name@company.com" />
                            <p>{errors.email?.message}</p>
                        </div>
                        <div>
                            <label htmlFor="password" className="block mb-2 text-sm font-bold text-third">Password</label>
                            <input {...register('password')} type="password" name="password" id="password" placeholder="••••••••" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " />
                            <p>{errors.password?.message}</p>
                        </div>
                        <div>
                            <label htmlFor="confirmPassword" className="block mb-2 text-sm font-bold text-third">Confirm Password</label>
                            <input {...register('confirmPassword')} type="password" name="confirmPassword" id="confirmPassword" placeholder="••••••••" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " />
                            <p>{errors.confirmPassword?.message}</p>
                        </div>
                        <button type="submit" className="w-full text-body bg-secondary hover:bg-third focus:ring-1 focus:outline-none focus:ring-third font-bold rounded-lg text-sm px-5 py-2.5 text-center">Sign up</button>
                        <p className="text-sm font-medium text-third">
                            Already have an account? <Link href="/auth/login" className="font-bold text-secondary hover:underline">Sign in</Link>
                        </p>
                    </form>
                </div>
            </div>
        </div>
    )
}