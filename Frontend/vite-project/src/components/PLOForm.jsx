import { zodResolver } from "@hookform/resolvers/zod";
import { set, useForm } from "react-hook-form";
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
import { addPLO, updatePLO } from "@/api/api-plo";
// import { useNavigate } from "react-router-dom";

const formSchema = z.object({
  ten: z.string().min(2, {
    message: "Ten must be at least 2 characters.",
  }),
  moTa: z.string().min(2, {
    message: "MoTa must be at least 2 characters.",
  }),
  cTDTId: z.coerce.number(
    {
      message: "Lop Hoc Phan Id must be a number",
    }
  ).min(1, {
    message: "Lop Hoc Phan Id must be at least 1 characters.",
  }),
});

export function PLOForm({ pLOId, handleAdd, handleEdit, setIsDialogOpen }) {
  // 1. Define your form.
  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: {
      id: pLOId,
      ten: "",
      moTa:"",
      cTDTId:"",
    },
  });

  // 2. Define a submit handler.
  async function onSubmit(values) {
    // Do something with the form values.
    // ✅ This will be type-safe and validated.
    if (pLOId) {
      const data = await updatePLO(pLOId, values);
      handleEdit(data);
    } else {
      const data = await addPLO(values);
      handleAdd(data);
      setIsDialogOpen(false);
    }
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {pLOId && (
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
                <Input placeholder="Nguyễn Văn A" {...field} />
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="moTa"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Mô Tả</FormLabel>
              <FormControl>
                <Input placeholder="Nguyễn Văn A" {...field} />
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="cTDTId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Lớp Học Phần Id</FormLabel>
              <FormControl>
                <Input placeholder="1" {...field} />
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
