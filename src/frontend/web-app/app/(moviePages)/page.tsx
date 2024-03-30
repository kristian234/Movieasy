import { getSession } from "../actions/auth-actions";
import { redirect } from "next/navigation";

export default async function Home() {

  const session = await getSession();

  if (session?.user && !session?.error) {
    redirect('/home')
  }

  return (
    <div>
      <h1>Normal page for non-logged in</h1>
    </div>
  )
}