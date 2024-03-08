'use client'

import Link from "next/link"
import { FaSignInAlt } from "react-icons/fa"

export default function LoginButton() {
    return (
        <div> 
            <Link className="text-secondary" href='/auth/login'>
                <FaSignInAlt className="mr-2" />
                Login
            </Link>
        </div>
    )
}