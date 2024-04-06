import Link from "next/link";
import { getSession } from "../actions/auth-actions";
import { redirect } from "next/navigation";
import { RiMovieLine } from "react-icons/ri";
import { Fragment } from "react";

export default async function Home() {

  const session = await getSession();

  if (session?.user && !session?.error) {
    redirect('/home')
  }

  return (
    <Fragment>
      <div className="flex justify-center items-center min-h-screen bg-gradient-to-r from-header via-darkHeader to-header overflow-hidden">
        <div className="relative flex flex-col items-center space-y-4" style={{ top: "-80px" }}>

          <div className="flex items-center text-secondary">
            <RiMovieLine className="text-3xl ml-2" />
            <span className="text-3xl font-bold">Movieasy</span>
          </div>

          <div className="text-center">
            <h2 className="text-2xl text-white">Your one-stop destination for movie reviews and news.</h2>
          </div>

          <div className="flex space-x-4">
            <Link href="/auth/register">
              <h2 className="bg-secondary hover:bg-third text-white font-bold py-2 px-4 rounded">
                Sign Up
              </h2>
            </Link>
            <Link href="/auth/login">
              <h2 className="bg-secondary hover:bg-third text-white font-bold py-2 px-4 rounded">
                Log In
              </h2>
            </Link>
          </div>

        </div>
        <div className="absolute bottom-0 right-0 mb-8 mr-32">
          <div className="w-64 h-64 bg-secondary rounded-full opacity-10"></div>
          <div className="w-48 h-48 bg-secondary rounded-full opacity-20 mt-10 ml-10"></div>
          <div className="w-32 h-32 bg-third rounded-full opacity-30 mt-10 ml-20"></div>
          <div className="w-16 h-16 bg-third rounded-full opacity-40 mt-10 ml-30"></div>
          <div className="w-8 h-8 bg-third rounded-full opacity-50 mt-10 ml-40"></div>
        </div>
      </div>

      <footer className="bg-transparent text-secondary py-8">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center">
            <div className="flex items-center">
              <RiMovieLine className="text-3xl mr-2" />
              <span className="text-xl font-bold">Movieasy</span>
            </div>
            <div className="flex space-x-4">
              <Link href="/about">
                <h2 className="hover:text-secondary">About Us</h2>
              </Link>
              <Link href="/contact">
                <h2 className="hover:text-secondary">Contact</h2>
              </Link>
              <Link href="/terms">
                <h2 className="hover:text-secondary">Terms</h2>
              </Link>
            </div>
          </div>
          <div className="mt-4">
            <p className="text-sm">2024 Movieasy.</p>
          </div>
        </div>
      </footer>

    </Fragment>
  )
}