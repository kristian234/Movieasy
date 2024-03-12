'use client'

import { registerUser } from "@/app/actions/auth-actions";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useState } from "react";

export default function RegisterForm() {
    const [email, setUsername] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const router = useRouter();

    const handleSubmit = async (e: any) => {
        e.preventDefault();
        
        registerUser(firstName, lastName, email, password).then(res => {
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
                    <form onSubmit={handleSubmit} className="space-y-4 md:space-y-6" action="#">
                        <div className="flex justify-between">
                            <div>
                                <label htmlFor="name" className="block mb-2 text-sm font-bold text-third">First Name</label>
                                <input autoComplete='off' onChange={(e) => setFirstName(e.target.value)} value={firstName} type="name" name="name" id="name" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " placeholder="Jonathan" />
                            </div>
                            <div className="ml-1">
                                <label htmlFor="name" className="block mb-2 text-sm font-bold text-third">Last Name</label>
                                <input autoComplete='off' onChange={(e) => setLastName(e.target.value)} value={lastName} type="name" name="name" id="name" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " placeholder="Dale" />
                            </div>
                        </div>
                        <div>
                            <label htmlFor="email" className="block mb-2 text-sm font-bold text-third">Your email</label>
                            <input autoComplete='off' onChange={(e) => setUsername(e.target.value)} value={email} type="email" name="email" id="email" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " placeholder="name@company.com" />
                        </div>
                        <div>
                            <label htmlFor="password" className="block mb-2 text-sm font-bold text-third">Password</label>
                            <input onChange={(e) => setPassword(e.target.value)} value={password} type="password" name="password" id="password" placeholder="••••••••" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " />
                        </div>
                        <div>
                            <label htmlFor="password" className="block mb-2 text-sm font-bold text-third">Confirm Password</label>
                            <input onChange={(e) => setConfirmPassword(e.target.value)} value={confirmPassword} type="password" name="confirmPassword" id="confirmPassword" placeholder="••••••••" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " />
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