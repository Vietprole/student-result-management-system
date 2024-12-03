import * as React from "react"
import { Check, ChevronsUpDown } from "lucide-react"

import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from "@/components/ui/command"
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"

const items = [
  {
    selectedId: "next.js",
    label: "Next.js",
  },
  {
    selectedId: "sveltekit",
    label: "SvelteKit",
  },
  {
    selectedId: "nuxt.js",
    label: "Nuxt.js",
  },
  {
    selectedId: "remix",
    label: "Remix",
  },
  {
    selectedId: "astro",
    label: "Astro",
  },
]

export function ComboBox({ items }) {
  const [open, setOpen] = React.useState(false)
  const [selectedId, setSelectedId] = React.useState(null)

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger asChild>
        <Button
          variant="outline"
          role="ComboBox"
          aria-expanded={open}
          className="w-[200px] justify-between"
        >
          {selectedId
            ? items.find((item) => item.id === selectedId)?.label
            : "Select item..."}
          <ChevronsUpDown className="opacity-50" />
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-[200px] p-0">
        <Command>
          <CommandInput placeholder="Search item..." />
          <CommandList>
            <CommandEmpty>No item found.</CommandEmpty>
            <CommandGroup>
              {items.map((item) => (
                <CommandItem
                  key={item.id}
                  value={item.label}
                  onSelect={() => {
                    setSelectedId(item.id)
                    setOpen(false)
                  }}
                >
                  {item.label}
                  <Check
                    className={cn(
                      "ml-auto",
                      selectedId === item.id ? "opacity-100" : "opacity-0"
                    )}
                  />
                </CommandItem>
              ))}
            </CommandGroup>
          </CommandList>
        </Command>
      </PopoverContent>
    </Popover>
  )
}
