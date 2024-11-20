import { SidebarTrigger } from "@/components/sidebar"

export default function Header() {
  return (
    <header className="bg-blue-500 text-white h-[60px] flex items-center p-4 shadow-md">
      <SidebarTrigger/>
      <h1 className="text-2xl font-bold">This is Header</h1>
    </header>
  );
}
