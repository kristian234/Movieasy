'use client'

import { setCookie } from "cookies-next";
import { getSession, signIn } from "next-auth/react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';

interface IFormInput {
    email: string;
    password: string;
    rememberMe: boolean;
}

const schema = yup.object().shape({
    email: yup.string().email().required(),
    password: yup.string().min(2).required(),
    rememberMe: yup.boolean().required(),
});

export default function LoginForm() {
    const router = useRouter();
    const { register, handleSubmit, formState: { errors } } = useForm<IFormInput>({
        resolver: yupResolver(schema)
    });

    const onSubmit = async (data: IFormInput) => {
        setCookie('rememberMe', data.rememberMe);

        const result = await signIn("credential", { email: data.email, password: data.password, redirect: false });

        const session = await getSession();

        console.log(session);
        if (!result?.error) {
            router.push("/")
            router.refresh();
        }
    };

    return (
        <div className="flex justify-center p-6 mt-10">
            <div className="w-full bg-header rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0">
                <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                    <h1 className="text-xl font-bold leading-tight tracking-tight text-secondary md:text-2xl">
                        Sign in to your account
                    </h1>
                    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 md:space-y-6" action="#">
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
                        <div className="flex items-center justify-between">
                            <div className="flex items-start">
                                <div className="flex items-center h-5">
                                    <input {...register('rememberMe')} id="rememberMe" aria-describedby="rememberMe" type="checkbox" className="w-4 h-4 border form-checkbox text-secondary accent-gray-700 border-secondary rounded focus:outline-none focus:ring-0 focus:ring-third" />
                                </div>
                                <div className="ml-3 text-sm">
                                    <label htmlFor="remember" className=" text-third">Remember me</label>
                                </div>
                            </div>
                        </div>
                        <button type="submit" className="w-full text-body bg-secondary hover:bg-third focus:ring-1 focus:outline-none focus:ring-third font-bold rounded-lg text-sm px-5 py-2.5 text-center">Sign in</button>
                        <p className="text-sm font-medium text-third">
                            Don’t have an account yet? <Link href="/auth/register" className="font-bold text-secondary hover:underline">Sign up</Link>
                        </p>
                    </form>
                </div>
            </div>
        </div>
    )
}
