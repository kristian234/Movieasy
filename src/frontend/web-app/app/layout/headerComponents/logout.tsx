'use client'

import { signOut } from "next-auth/react"
import Link from "next/link"
import { FiLogOut } from "react-icons/fi"

export default function LogoutButton() {


    return (
        <div>
            <button  className='text-secondary flex items-center' onClick={() => signOut()}>
                <FiLogOut className="mr-2" />
                Logout
            </button>
        </div>
    )
}