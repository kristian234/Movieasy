import { Metadata } from "next";
import "../../globals.css";
import ToasterProvider from "@/app/Providers/ToasterProvider";
import ModalWrapper from "@/app/Providers/ModalProvider";

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
            <ToasterProvider />
            <ModalWrapper>
              {children}
            </ModalWrapper>
          </main>
        </div>
      </body>
    </html>
  )
}

