import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { addKhoa, updateKhoa } from "@/api/api-khoa";

const formSchema = z.object({
  ten: z.string().min(2, {
    message: "Ten must be at least 2 characters.",
  }),
  maKhoa: z.coerce.number({
    message: "MaKhoa must be a number.",
  }).min(3, {
    message: "MaKhoa must be at least 3 characters.",
  }),
});

export function KhoaForm({ khoa, handleAdd, handleEdit, setIsDialogOpen }) {
  // 1. Define your form.
  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: khoa || {
      ten: "",
      maKhoa: "",
    },
  });

  // 2. Define a submit handler.
  async function onSubmit(values) {
    // Do something with the form values.
    // ✅ This will be type-safe and validated.
    console.log("values 43: ", values);
    if (khoa.id) {
      console.log("values", values);
      const data = await updateKhoa(khoa.id, values);
      handleEdit(data);
    } else {
      console.log("values", values);
      const data = await addKhoa(values);
      handleAdd(data);
      setIsDialogOpen(false);
    }
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {khoa && (
          <FormField
            control={form.control}
            name="id"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Id</FormLabel>
                <FormControl>
                  <Input {...field} readOnly/>
                </FormControl>
                <FormDescription>
                  This is your unique identifier.
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        <FormField
          control={form.control}
          name="ten"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Tên</FormLabel>
              <FormControl>
                <Input placeholder="Tên Khoa (vd: Công nghệ thông tin)..." {...field} />
              </FormControl>
              <FormDescription>
                Đây là mục nhập tên Khoa.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="maKhoa"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Mã Khoa</FormLabel>
              <FormControl>
                <Input placeholder="101" {...field} readOnly={!!khoa}/>
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit">Submit</Button>
      </form>
    </Form>
  );
}
