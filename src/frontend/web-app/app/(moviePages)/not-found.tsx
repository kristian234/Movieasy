import Link from 'next/link'

export default function NotFound() {
  return (
    <main className="flex h-full flex-col items-center justify-center">
      <div className="flex justify-center mt-8 bg-black bg-opacity-15 shadow-3xl max-w-4xl p-8 rounded-lg">
        <div className="flex flex-col items-center justify-center h-full">
          <h1 className="text-4xl font-bold text-primary">Not Found</h1>
          <p className="text-lg text-gray-600 mt-4">Could not find the requested resource.</p>
          <Link href="/" className="mt-6 inline-block px-6 py-3 text-sm font-semibold text-primary transition-colors bg-secondary rounded-md hover:bg-third">
            Return Home
          </Link>
        </div>
      </div>
    </main>
  )
}