import { Metadata } from "next";
import "../../globals.css";

export const metadata: Metadata = {
  title: "Movieasy",
  description: "Movieasy",
};

export default async function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      <body>
        <div className="min-h-screen flex-auto max-h-screen overflow-auto">
          <main>
            {children}
          </main>
        </div>
      </body>
    </html>
  )
}

