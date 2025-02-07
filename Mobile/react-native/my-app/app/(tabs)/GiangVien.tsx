import React, { useState } from 'react';
import { Alert, FlatList, ScrollView, StyleSheet, Text, TouchableOpacity, View, SafeAreaView, Platform, StatusBar, Image } from 'react-native';
import { router } from 'expo-router';
import { SelectList } from 'react-native-dropdown-select-list';
import { apiClient } from '../api/apiClient';
import Header from '../common/Header';
import Footer from '../common/Footer';

const handleFooterPress = (route: string) => {
  if (route === 'home') {
    router.push('/(tabs)/Home');
  } else if (route === 'person') {
    router.push('/(tabs)/Profile');
  }
};

type TableRow = {
  TT: string;
  GV: string;
  Khoa: string;
  HocPhan: string;
};

type ColumnWidths = {
  [key: string]: number;
};

const Result = () => {
  const [selected, setSelected] = React.useState("");
  const tableHeaders = ['TT', 'Giảng viên', 'Khoa', 'Học phần'];
  const tableData: TableRow[] = [
    { TT: '1', GV: 'Đặng Thiên Bình', Khoa: 'CNTT', HocPhan: 'PBL6' },
    { TT: '2', GV: 'Võ Thị Liên', Khoa: 'CNTT', HocPhan: 'Công nghệ phần mềm' },
    { TT: '3', GV: 'Võ Đức Hoàng', Khoa: 'CNTT', HocPhan: 'Kiểm thử phần mềm' },
    { TT: '4', GV: 'Đặng Hoài Phương', Khoa: 'CNTT', HocPhan: 'Kiến trúc hướng dịch vụ' },
    { TT: '5', GV: 'Lê Thị Mỹ Hạnh', Khoa: 'CNTT', HocPhan: "Lập trình hướng đối tượng" },
  ];
  const semesters = [
    { key: '1', value: 'Học kỳ 1 - Năm 2024-2025' },
    { key: '2', value: 'Học kỳ 2 - Năm 2023-2024' },
    { key: '3', value: 'Học kỳ 1 - Năm 2023-2024' },
  ];
  const columnWidths: ColumnWidths = {
    TT: 40,
    GV: 160,
    Khoa: 80,
    HocPhan: 200,
  };

  const headerToWidthKey: { [key: string]: string } = {
    'TT': 'TT',
    'Giảng viên': 'GV',
    'Khoa': 'Khoa',
    'Học phần': 'HocPhan',
  };

  const renderHeader = () => (
    <View style={[styles.row, styles.headerRow]}>
      {tableHeaders.map((header, index) => (
        <Text
          key={index}
          style={[
            styles.cell,
            styles.headerCell,
            { width: columnWidths[headerToWidthKey[header]] }
          ]}
        >
          {header}
        </Text>
      ))}
    </View>
  );

  const renderRow = ({ item, index }: { item: TableRow; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={[styles.cellTT, { width: columnWidths.TT }]}>{item.TT}</Text>
      <Text style={[styles.cell, { width: columnWidths.GV }]}>{item.GV}</Text>
      <Text style={[styles.cell, { width: columnWidths.Khoa }]}>{item.Khoa}</Text>
      <Text style={[styles.cell, { width: columnWidths.HocPhan }]}>{item.HocPhan}</Text>
    </View>
  );

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        {/* Header */}
        <Header title="GIẢNG VIÊN" />

        {/* Dropdown Section */}
        <View style={styles.dropdownContainer}>
          <SelectList
            setSelected={setSelected}
            data={semesters}
            placeholder="Chọn học kỳ"
            searchPlaceholder="Tìm học kỳ"
            save="value"
            boxStyles={styles.dropdown}
            dropdownStyles={styles.dropdownList}
          />
          <TouchableOpacity
            style={styles.selectButton}
            onPress={() => {
              if (selected) {
                console.log("Selected semester:", selected);
              } else {
                Alert.alert("Thông báo", "Vui lòng chọn học kỳ");
              }
            }}
          >
            <Text style={styles.selectButtonText}>Chọn</Text>
          </TouchableOpacity>
        </View>

        {/* Table Content */}
        <ScrollView horizontal>
          <View style={styles.table}>
            {renderHeader()}
            <FlatList
              data={tableData}
              renderItem={renderRow}
              keyExtractor={(item, index) => index.toString()}
            />
          </View>
        </ScrollView>

        {/* Footer */}
        <Footer />
      </View>
    </SafeAreaView>
  );
};

const footerItems: { icon: 'home' | 'person', label: string }[] = [
  { icon: 'home', label: 'Trang chủ' },
  { icon: 'person', label: 'Hồ sơ' },
];

const styles = StyleSheet.create({
  table: {
    borderWidth: 1,
    borderColor: '#ccc',
    marginHorizontal: 16,
    borderRadius: 8,
    overflow: 'hidden',
    margin: 16,
  },
  row: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
  },
  cellTT: {
    padding: 10,
    textAlign: 'center',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
    alignItems: 'center',
  },
  cell: {
    padding: 10,
    textAlign: 'left',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
  },
  headerRow: {
    backgroundColor: '#f1f1f1',
    borderBottomWidth: 2,
    borderBottomColor: '#ccc',
  },
  headerCell: {
    fontWeight: 'bold',
    fontSize: 14,
    backgroundColor: '#f1f1f1',
    textAlign: 'center',
  },
  evenRow: {
    backgroundColor: '#f9f9f9',
  },
  oddRow: {
    backgroundColor: '#ffffff',
  },
  link: {
    color: 'blue',
    textDecorationLine: 'underline',
    textAlign: 'center',
  },
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
  },
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  dropdownContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    gap: 10,
  },
  dropdown: {
    flex: 1,
    borderColor: '#0000cc',
    borderRadius: 8,
    height: 50,
    paddingHorizontal: 12,
  },
  dropdownList: {
    borderColor: '#0000cc',
    marginTop: 5,
    maxHeight: 150,
  },
  selectButton: {
    backgroundColor: '#0000cc',
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
    height: 43,
  },
  selectButtonText: {
    color: 'white',
    fontWeight: 'bold',
    fontSize: 16,
  },
});

export default Result;